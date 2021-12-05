using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CaffStore.Bll
{
    [Testclass]
    public class UnitTestBll
    {
        private readonly StoreDbContext context;
        private readonly IUserService userService;


        [TestMethod]
        public void UserNameReturnString()
        {
            
            var userService = new UserService();
            var result = userService.GetCurrentUserName();
            
            var boolean = false;

            if(result is string){
            boolean = true;
            }

            Assert.IsTrue(boolean, "Username needs to be string.");
        }

        [TestMethod]
        public void UserRoleString()
        {
            
            var userService = new UserService();
            var result = userService.GetCurrentUserRole();
            
            var boolean = false;

            if(result is string){
            boolean = true;
            }

            Assert.IsTrue(boolean, "User role needs to be string.");
        }

        [TestMethod]
        public void UserIdString()
        {
            
            var userService = new UserService();
            var result = userService.GetCurrentUserId();
            
            var boolean = false;

            if(result is string){
            boolean = true;
            }
            
            Assert.IsTrue(boolean, "User ID needs to be string.");
        }

        [TestMethod]
        public void TestGetUploadedImageById(int id_)
        {   
            var storeService = new StoreService(context,iuserservice);
            var id = id_;
            var result = storeService.GetUploadedImageById(id);
            var boolean = false;
            if (result.Id == id){
            boolean = true;
            }
            Assert.IsTrue(boolean, "Result id must be equal to id");
        }
        
                [TestMethod]
        public void TestGetUploadedImageByNonExistingId(int id_)
        {   
            var storeService = new StoreService(context,iuserservice);
            var id = id_;
            var result = storeService.GetUploadedImageById(id);
            var boolean = true;
            if (result.Id == id){
            boolean = false;
            }
            Assert.IsTrue(boolean, "Result id must not be equal");
        }

        [TestMethod]

        public void TestComment(Guid imageID, CommentDto comment, int date_){

            var storeService = new StoreService(context,iuserservice);
            var result = storeService.CreateComment(id_);
        
            Assert.IsTrue(result.Any(x => x.CommentDate == date_));
        }


        [TestMethod]
        public void TestLoadCaffJson()
        {   
            var storeService = new StoreService(context,iuserservice);
            var id = 1;
            var result = storeService.LoadCaffJson(id);
            var boolean = false;
            if (result is CaffJson){
            boolean = true;
            }
            Assert.IsTrue(boolean, "Result must be CaffJson");
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "A kép feldolgozása nem sikerült.")]
        public void TestCaffBadUpload(){

            CaffFile file;
            string output = "";

            var storeService = new StoreService();

            storeService.SaveCaffIfSuccesful(output, file);

        }


    }
}