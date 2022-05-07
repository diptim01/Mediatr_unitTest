// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Contracts;
using Assessment.Console;
using Domain;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Utility;
using Newtonsoft.Json;

await new ConsoleLeadsGeneration().Process();