﻿namespace Contracts.Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public string? OrderBy { get; set; }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}
