namespace GloboEvent.Api.IntegrationTest.Controllers.Category.Command
{
    internal class CreateCategoryCommand : Application.Features.Categories.Commands.Create.CreateCategoryCommand
    {
        public string Name { get; set; }
    }
}