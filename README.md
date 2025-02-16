@typeparam TItem

<div>
    <table class="table">
        <thead>
            @ChildContentHeader
        </thead>
        <tbody>
            @foreach (var item in PaginatedData)
            {
                @ChildContentRow(item)
            }
        </tbody>
    </table>

    <div class="pagination-controls">
        <!-- Page Size Dropdown -->
        <label for="pageSize">Page Size: </label>
        <select @bind="PageSize" id="pageSize">
            @foreach (var size in PageSizes)
            {
                <option value="@size">@size</option>
            }
        </select>

        <!-- Previous Page Button -->
        <button @onclick="PreviousPage" disabled="@IsPreviousDisabled">Previous</button>

        <!-- Page Number Buttons -->
        @if (TotalPages > 0)
        {
            int startPage = Math.Max(1, CurrentPage - 2);
            int endPage = Math.Min(TotalPages, startPage + 4);

            if (endPage - startPage < 4 && startPage > 1)
            {
                startPage = Math.Max(1, endPage - 4);
            }

            if (startPage > 1)
            {
                <button @onclick="() => GoToPage(1)">1</button>
                @if (startPage > 2)
                {
                    <span>...</span>
                }
            }

            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                <button @onclick="() => GoToPage(pageNumber)" class="@(CurrentPage == pageNumber ? "active" : "")">
                    @pageNumber
                </button>
            }

            if (endPage < TotalPages)
            {
                if (endPage < TotalPages - 1)
                {
                    <span>...</span>
                }
                <button @onclick="() => GoToPage(TotalPages)">@TotalPages</button>
            }
        }

        <!-- Next Page Button -->
        <button @onclick="NextPage" disabled="@IsNextDisabled">Next</button>
    </div>
</div>

@code {
    [Parameter] public IEnumerable<TItem> Items { get; set; }
    [Parameter] public int DefaultPageSize { get; set; } = 10;
    [Parameter] public RenderFragment ChildContentHeader { get; set; }
    [Parameter] public RenderFragment<TItem> ChildContentRow { get; set; }

    private int PageSize { get; set; }
    private List<TItem> PaginatedData => Items.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
    private int CurrentPage { get; set; } = 1;

    private readonly int[] PageSizes = new[] { 5, 10, 20, 50 };

    protected override void OnInitialized()
    {
        PageSize = DefaultPageSize;
    }

    private void NextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;
        }
    }

    private void PreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
        }
    }

    private void GoToPage(int pageNumber)
    {
        CurrentPage = pageNumber;
    }

    private int TotalPages => (int)Math.Ceiling(Items.Count() / (double)PageSize);

    private bool IsPreviousDisabled => CurrentPage == 1;
    private bool IsNextDisabled => CurrentPage == TotalPages;
}
