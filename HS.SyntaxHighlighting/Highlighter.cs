using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace HS.SyntaxHighlighting
{
    public class Highlighter : IHighlighter
    {
        private static readonly Dictionary<string, string> LanguageKeyToName = new Dictionary<string, string>
        {
            { "awk", "(G)AWK script" },
            { "ada", "ADA 95" },
            { "ampl", "AMPL" },
            { "amtrix", "AMTrix" },
            { "arm", "ARM" },
            { "asp", "ASP" },
            { "aspect", "Abstract" },
            { "as", "Action Script" },
            { "agda", "Agda" },
            { "httpd", "Apache Conf" },
            { "applescript", "AppleScript" },
            { "asm", "Assembler" },
            { "au3", "AutoIt 3" },
            { "avenue", "Avenue" },
            { "y", "BISON" },
            { "bms", "BM Script" },
            { "sh", "Bash script" },
            { "bib", "BibTex" },
            { "bb", "Blitz Basic 3D" },
            { "c", "C and C++" },
            { "cs", "C#" },
            { "cob", "COBOL (ANSI 74/85)" },
            { "css", "CSS" },
            { "cb", "ClearBasic" },
            { "clipper", "Clipper" },
            { "clp", "Clips" },
            { "cfc", "Coldfusion MX" },
            { "d", "D" },
            { "bat", "DOS Batch script" },
            { "diff", "Diff" },
            { "dylan", "Dylan" },
            { "e", "Eiffel" },
            { "erl", "Erlang" },
            { "euphoria", "Euphoria Script" },
            { "exp", "Express" },
            { "inp", "FAME" },
            { "flx", "Felix" },
            { "f77", "Fortran 77" },
            { "f90", "Fortran 90" },
            { "frink", "Frink" },
            { "haskell", "Haskell" },
            { "hcl", "Hecl" },
            { "idl", "IDL" },
            { "4gl", "INFORMIX" },
            { "ini", "INI" },
            { "io", "IO" },
            { "icn", "Icon" },
            { "jsp", "JSP 2.0" },
            { "j", "Jasmin" },
            { "java", "Java" },
            { "js", "Javascript" },
            { "ldif", "LDAP LDIF  Script" },
            { "lua", "LUA" },
            { "lsl", "Linden Script" },
            { "lisp", "Lisp" },
            { "lotos", "Lotos (ISO 8807)" },
            { "ls", "Lotus Script" },
            { "lbn", "Luban" },
            { "ps1", "MS PowerShell" },
            { "make", "Make script" },
            { "mpl", "Maple" },
            { "m", "Matlab" },
            { "ms", "MaxScript" },
            { "mel", "Maya script" },
            { "miranda", "Miranda" },
            { "mo", "Modelica script" },
            { "mod3", "Modula 3" },
            { "nas", "Nasal" },
            { "n", "Nemerle" },
            { "nice", "Nice" },
            { "oberon", "Oberon" },
            { "os", "Object Script" },
            { "objc", "Objective C" },
            { "ml", "Objective Caml" },
            { "octave", "Octave" },
            { "oorexx", "Open Object Rexx" },
            { "psl", "PATROL" },
            { "php", "PHP3/4/5" },
            { "pl1", "PL/1 script" },
            { "pov", "POV-Ray 3.1" },
            { "sc", "Paradox script" },
            { "pas", "Pascal/Object Pascal" },
            { "pl", "Perl" },
            { "pike", "Pike" },
            { "plain-text", "Plain text" },
            { "ps", "Post Script" },
            { "s", "PowerPC Assembler" },
            { "progress", "Progress script" },
            { "pro", "Prolog" },
            { "pyx", "Pyrex" },
            { "py", "Python" },
            { "boo", "Python" },
            { "q", "Qore" },
            { "qu", "Qu" },
            { "r", "R" },
            { "spec", "RPM Spec" },
            { "rexx", "Rexx" },
            { "ruby", "Ruby" },
            { "rb", "Ruby" },
            { "abp", "SAP - ABAP/4" },
            { "sas", "SAS" },
            { "sma", "SMALL" },
            { "snobol", "SNOBOL" },
            { "spn", "SPIN SQL" },
            { "sql", "SQL, PL/SQL" },
            { "sybase", "SYBASE SQL" },
            { "scala", "Scala" },
            { "scilab", "Scilab" },
            { "nut", "Squirrel" },
            { "sml", "Standard ML" },
            { "xpp", "Superx++" },
            { "mssql", "T-SQL" },
            { "ttcn3", "TTCN-3" },
            { "tcl", "Tcl/Tk" },
            { "tcsh", "Tcsh script" },
            { "tex", "TeX/LaTeX" },
            { "vhd", "VHDL" },
            { "verilog", "Verilog" },
            { "vb", "Visual Basic" },
            { "xml", "XML" }
        };

        private readonly string pathToHighlightExe;

        public Highlighter(string pathToHighlightExe)
        {
            this.pathToHighlightExe = pathToHighlightExe;
        }

        public IDictionary<string, string> Languages
        {
            get
            {
                return LanguageKeyToName;
            }
        }

        public string Highlight(string text, string languageKey)
        {
            try
            {
                if (languageKey == "plain-text")
                    return HttpUtility.HtmlEncode(text);

                if (!LanguageKeyToName.ContainsKey(languageKey))
                    throw new HighlightException("Unknown language: " + languageKey);

                return HighlightWithProgram(text, languageKey);
            }
            catch (Exception ex)
            {
                throw new HighlightException("Unable to highlight text: " + ex.Message, ex);
            }
        }

        private string HighlightWithProgram(string text, string language)
        {
            var process = Process.Start(
                new ProcessStartInfo
                    {
                        FileName = pathToHighlightExe,
                        Arguments = ("-S" + language + " -X -t 4 -f"),
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    }
                );

            if (process == null)
            {
                throw new HighlightException("Unable to start highlighter process. No error given.");
            }

            process.StandardInput.Write(text);
            process.StandardInput.Close();

            return process.StandardOutput.ReadToEnd();
        }
    }
}