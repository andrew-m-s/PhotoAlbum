using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoAlbum.Tests;

[TestClass]
public class Verify
{
    [TestMethod]
    public void VerifyUnitTestsAreRunningInAutomation()
    {
        // Created as a test to verify unit tests are running during automation
        Assert.AreEqual(true, true);
    }
}