﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace FileManager2
{
    class Program
    {
        static void Main(string[] args)
        {
            FMI filemanager = new FMI();
            filemanager.selectdrive();
        }
    }
}
