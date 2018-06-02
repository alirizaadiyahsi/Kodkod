namespace Kodkod.Application.Dto
{
    //todo: think about where this class should be placed, Utilities project is more convenient?
    public class PagedListInput
    {
        public PagedListInput()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public string Filter { get; set; }

        public string Sorting { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
