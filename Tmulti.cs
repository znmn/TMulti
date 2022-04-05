// See https://aka.ms/new-console-template for more information
using System;
using TeleMulti;

string acc = "./acc";
string defFolder = "./folder";
string pFolder = "./profiles";

TMulti tele = new(acc, defFolder, pFolder);
tele.Start();
