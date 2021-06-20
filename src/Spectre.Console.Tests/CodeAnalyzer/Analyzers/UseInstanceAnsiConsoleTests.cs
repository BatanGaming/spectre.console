using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Spectre.Console.Analyzer;
using Xunit;
using AnalyzerVerify =
    Spectre.Console.Tests.CodeAnalyzers.SpectreAnalyzerVerifier<
        Spectre.Console.Analyzer.FavorInstanceAnsiConsoleOverStaticAnalyzer>;

namespace Spectre.Console.Tests.CodeAnalyzers.Analyzers
{
    public class FavorInstanceAnsiConsoleOverStaticAnalyzerTests
    {
        private static readonly DiagnosticResult _expectedDiagnostics = new(
            Descriptors.S1010_FavorInstanceAnsiConsoleOverStatic.Id,
            DiagnosticSeverity.Info);

        [Fact]
        public async void Console_Write_Has_Warning()
        {
            const string Source = @"
using Spectre.Console;

class TestClass 
{
    IAnsiConsole _ansiConsole = AnsiConsole.Console;    

    void TestMethod() 
    {
        _ansiConsole.Write(""this is fine"");
        AnsiConsole.Write(""Hello, World"");
    } 
}";

            await AnalyzerVerify
                .VerifyAnalyzerAsync(Source, _expectedDiagnostics.WithLocation(11, 9))
                .ConfigureAwait(false);
        }
    }
}