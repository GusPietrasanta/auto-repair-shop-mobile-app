﻿using System;

namespace App.Models;

public class Message
{
    public string UserName { get; set; }
    public string Content { get; set; }
    public DateTime PostedOn { get; set; }
    public string Tag { get; set; }
}