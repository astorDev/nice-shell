using Console = NiceShell.Console;

Console.WriteLogLine("Calculating the output list");
Console.WriteOutputLine("Line 1");
Console.WriteOutputLine("Line 2");

Console.WriteLogLine("Calculating single output 1");
Console.WriteOutputAndNewLogLine("Single Line Output");

Console.WriteWarningLine("Calculating something dangerous");
Console.WriteOutputAndNewLogLine("Something dangerous");
