using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using MyTested.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Services;
using PCHUBStore.Controllers;
using PCHUBStore.Data;
using PCHUBStore.Mappings;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using PCHUBStore.View.Models.ApiViewModels;
using PCHUBStore.View.Models.IndexViewModels;
using Xunit;

namespace PCHUBStore.Tests.MyTestedAspNet
{
    public class MyTestedAspNetControllers
    {
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {

            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddActionContextAccessor();
                });
            var context = new PCHUBDbContext();

            var mock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> logger = mock.Object;

            var model = new IndexViewModel();
            
            //or use this short equivalent 
            logger = Mock.Of<ILogger<HomeController>>();
            
                var service = new HomeService(context);
                
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<AdminProfile>();
                    config.AddProfile<ProductsProfile>();
                    config.AddProfile<UsersProfile>();
                    config.AddProfile<ApiProfile>();
                });

            var mapper = new Mapper(mapperConfiguration);
  

            MyController<HomeController>
                .Instance(
                    instance => instance
                        .WithDependencies(
                            logger,
                            service,
                            mapper
                        ))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view.WithModelOfType<IndexViewModel>().Passing(model => model.Boxes.Any()));

            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Home/Index")
                    .WithMethod(HttpMethod.Get))
                .To<HomeController>(c => c.Index());

            MyController<HomeController>
                .Instance(instance => instance
                    .WithDependencies(
                        logger,
                        service,
                        mapper
                    )
                    .WithUser(user => user
                        .WithUsername("obelix")))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view.WithModelOfType<IndexViewModel>().Passing(model => model.Categories.Any()));

            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Home/Error")
                    .WithMethod(HttpMethod.Get))
                .To<HomeController>(c => c.Error());

            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Home/Privacy")
                    .WithMethod(HttpMethod.Get))
                .To<HomeController>(c => c.Privacy());


            // Task<ActionResult<List<ApiProductHistoryViewModel>>> ReviewedProducts()

            MyController<HomeController>
                .Instance(instance => instance
                    .WithDependencies(
                        logger,
                        service,
                        mapper
                    )
                    .WithUser(user => user
                        .WithUsername("obelix")))
                .Calling(c => c.ReviewedProducts())
                .ShouldReturn()
                .ActionResult(result => result.Object(obj => obj.WithModelOfType<List<ApiProductHistoryViewModel>>().Passing(x => x.Any())));
        }

    }
}
