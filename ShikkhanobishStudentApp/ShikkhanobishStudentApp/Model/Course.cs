﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShikkhanobishStudentApp.Model
{
    public class Course
    {
        public int degreeID { get; set; }
        public int courseID { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public int tuitionRequest { get; set; }
        public float avgRatting { get; set; }
        public int uniNameID { get; set; }
        public int indexNo { get; set; }
        public string Response { get; set; }
    }
}
