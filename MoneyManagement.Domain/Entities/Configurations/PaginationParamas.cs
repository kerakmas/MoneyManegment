﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Domain.Entities.Configurations
{
    public class PaginationParamas
    {
        private const short _maxSize = 20;
        private const short _minSize = 1;
        private short _pageSize = 1;
        private int _pageIndex = 1;
        public short PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxSize ? _maxSize : value < _minSize ? _minSize : value;
        }

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 0 ? _pageIndex : value;
        }
    }
}
