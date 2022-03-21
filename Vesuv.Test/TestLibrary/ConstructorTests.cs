using System.Reflection;

namespace Vesuv.TestLibrary
{
    public static class ConstructorTests<T>
    {
        public static Tester<T> For(params Type[] argTypes)
        {
            var ctor = typeof(T).GetConstructor(argTypes);
            if (ctor == null) {
                return new MissingCtorTester<T>();
            }

            return new CtorTester<T>(ctor);
        }
    }

    public abstract class Tester<T>
    {
        public abstract Tester<T> Fail(object[] args, Type exceptionType, string failMessage);
        public abstract Tester<T> Succeed(object[] args, string failMessage);
        public abstract void Assert();
    }

    public sealed class MissingCtorTester<T> : Tester<T>
    {
        public override Tester<T> Fail(object[] args, Type exceptionType, string failMessage)
        {
            return this;
        }

        public override Tester<T> Succeed(object[] args, string failMessage)
        {
            return this;
        }

        public override void Assert()
        {
            throw new Xunit.Sdk.XunitException("Missing constructor");
        }
    }

    public sealed class CtorTester<T> : Tester<T>
    {
        private readonly ConstructorInfo _ctorInfo;
        private readonly IList<TestCase<T>> _testCases = new List<TestCase<T>>();

        public CtorTester(ConstructorInfo ctorInfo)
        {
            _ctorInfo = ctorInfo;
        }

        public override Tester<T> Fail(object[] args, Type exceptionType, string failMessage)
        {
            var testCase = new FailTest<T>(_ctorInfo, args, exceptionType, failMessage);
            _testCases.Add(testCase);
            return this;
        }

        public override Tester<T> Succeed(object[] args, string failMessage)
        {
            var testCase = new SuccessTest<T>(_ctorInfo, args, failMessage);
            _testCases.Add(testCase);
            return this;
        }

        public override void Assert()
        {
            var errors = new List<string>();
            ExecuteTestCases(errors);
            Assert(errors);
        }

        private void ExecuteTestCases(List<string> errors)
        {
            foreach (var testCase in _testCases) {
                ExecuteTestCase(errors, testCase);
            }
        }

        private static void ExecuteTestCase(List<string> errors, TestCase<T> testCase)
        {
            var error = testCase.Execute();
            if (!String.IsNullOrEmpty(error)) {
                errors.Add($"    ----> {error}");
            }
        }

        private static void Assert(List<string> errors)
        {
            if (errors.Count > 0) {
                var error = $"{errors.Count} error(s) occurred:\n{String.Join("\n", errors.ToArray())}";
                throw new Xunit.Sdk.XunitException(error);
            }
        }
    }

    public abstract class TestCase<T>
    {
        private readonly ConstructorInfo _ctorInfo;
        private readonly object[] _args;
        private readonly string _failMessage;

        public TestCase(ConstructorInfo ctorInfo, object[] args, string failMessage)
        {
            _ctorInfo = ctorInfo;
            _args = args;
            _failMessage = failMessage;
        }

        protected T InvokeConstructor()
        {
            try {
                return (T)_ctorInfo.Invoke(_args);
            } catch (TargetInvocationException ex) {
                throw ex?.InnerException ?? new Exception();
            }
        }

        protected string Fail(string message)
        {
            return $"Test failed ({_failMessage}): {message}";
        }

        protected string Success()
        {
            return String.Empty;
        }

        public abstract string Execute();
    }

    public sealed class FailTest<T> : TestCase<T>
    {
        private readonly Type _exceptionType;

        public FailTest(ConstructorInfo ctorInfo, object[] args, Type exceptionType, string failMessage)
            : base(ctorInfo, args, failMessage)
        {
            _exceptionType = exceptionType;
        }

        public override string Execute()
        {
            try {
                _ = InvokeConstructor();
                return Fail($"{_exceptionType.Name} not thrown when expected.");
            } catch (Exception ex) {
                if (ex.GetType() != _exceptionType) {
                    return Fail($"{ex.GetType().Name} thrown when {_exceptionType.Name} was expected.");
                }
            }
            return Success();
        }
    }

    public sealed class SuccessTest<T> : TestCase<T>
    {
        public SuccessTest(ConstructorInfo ctorInfo, object[] args, string failMessage)
            : base(ctorInfo, args, failMessage)
        {
        }

        public override string Execute()
        {
            try {
                _ = InvokeConstructor();
            } catch (Exception ex) {
                return Fail($"{ex.GetType().Name} occured: {ex.Message}");
            }
            return Success();
        }
    }
}
