﻿using MyToDo2.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.Services
{
    public class MemoService : BaseService<MemoDto>, IMemoService
    {
        public MemoService(HttpRestClient client) : base(client, "Memo")
        {
        }
    }
}