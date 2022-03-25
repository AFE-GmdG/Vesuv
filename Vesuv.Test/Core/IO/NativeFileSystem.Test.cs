using Xunit;

namespace Vesuv.Core.IO
{

    public class NativeFileSystemTest
    {

        [Theory(DisplayName = @"Open a existing NativeFileSystem")]
        [InlineData(@"S:\Work\Vesuv\Playground")]
        public async Task OpenExistingNativeFileSystem(string resRootPath)
        {
            // Prepare Test

            // Prepare actual object
            var fs = await NativeFileSystem.InitializeNativeFileSystem(resRootPath);

            // Prepare expected properties
            UInt64 ProjectFileRID = 0UL;

            // Do the tests
            Assert.NotNull(fs);
            Assert.Equal(ProjectFileRID, fs.ProjectFile.RID);
        }

        [Theory(DisplayName = @"Try open a non-existing NativeFileSystem")]
        [InlineData(@"S:\Work\Vesuv\No project in this folder")]
        public async Task OpenNonExistingNativeFileSystem(string resRootPath)
        {
            // Do the tests
            await Assert.ThrowsAsync<DirectoryNotFoundException>(() => NativeFileSystem.InitializeNativeFileSystem(resRootPath));
        }
    }
}
