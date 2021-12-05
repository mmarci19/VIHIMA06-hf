using CaffStore.Api.Controllers;
using CaffStore.Bll.Dtos;
using CaffStore.Bll.Interfaces;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestProject2
{
    public class StoreControllerTest
    {
        [Fact]
        public async void browseImagesReturnNotNull()
        {
            //ARRANGE
            var fakeService = A.Fake<IStoreService>();
            var filter = "testFilter";
            var count = 2;
            var fakeImages = A.CollectionOfFake<UploadedImagesResponseDto>(count);
            A.CallTo(() => fakeService.GetUploadedImages(filter)).Returns(fakeImages);
            var controller = new StoreController(fakeService);

            //ACT
            var res = await controller.BrowseImages(filter);

            //ASSERT
            Assert.NotNull(res);
        }
        [Fact]
        public async void getImageByIdReturnNotNull()
        {
            //ARRANGE
            var fakeService = A.Fake<IStoreService>();
            var filter = Guid.NewGuid();
            var count = 2;
            var fakeImages = A.Fake<Task<DetailsDto>>();
            A.CallTo(() => fakeService.GetUploadedImageById(filter)).Returns(fakeImages);
            var controller = new StoreController(fakeService);

            //ACT
            var res = await controller.GetImageById(filter);

            //ASSERT
            Assert.NotNull(res);

        }
        [Fact]
        public async void AddCommentTest()
        {
            //ARRANGE
            var fakeService = A.Fake<IStoreService>();
            var guId = Guid.NewGuid();
            var fakeCommentDto = A.Fake<CommentDto>();
            var fakeTask = A.Fake<Task>();
            var fakeImages = A.Fake<Task<DetailsDto>>();
            A.CallTo(() => fakeService.CreateComment(guId, fakeCommentDto)).Returns(fakeTask);
            var controller = new StoreController(fakeService);

            //ACT
            var res = controller.AddComment(guId,fakeCommentDto);

            //ASSERT
            Assert.NotNull(res);
        }

    }
}
