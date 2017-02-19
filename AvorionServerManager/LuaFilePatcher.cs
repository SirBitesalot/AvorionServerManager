using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvorionServerManager.Commands;
namespace AvorionServerManager
{
    class LuaFilePatcher
    {
        public const string AvorionServerManagerModificationStart = "--asm_modification_start";
        public const string AvorionServerManagerModificationEnd = "--asm_modification_end";
        public const string AvorionServerManagerEditedNoticeStart = "--asm_editedNotice_start";
        public const string AvorionServerManagerEditedNoticeEnd = "--asm_editedNotice_end";

        public const string AvorionServerManagerCheckDelayIdentifier = "--asm_checkDelay";
        public const string AvorionServerManagerCheckCommandsFunctionIdentifier = "--asm_checkCommandsFunction";
        public const string AvorionServerManagerUpdateLogicIdentifier = "--asm_updateLogic";

        public const string VanillaOnStartupFunctionStart = "function onStartUp()";
        public const string VanillaUpdateFunctionStart = "function update(timeStep)";

        private const string _setCommandCheckDelayCodeInitial = "Server():setValue(\"commandCheckDelay\", 0.5)";

        private List<AvorionServerCommandDefinition> _commandDefinitions;
        public LuaFilePatcher(List<AvorionServerCommandDefinition> commandDefinitions)
        {
            _commandDefinitions = commandDefinitions;
        }
        public static List<string>  RemoveAsmPatches(List<string> serverLuaLines)
        {
            List<string> cleanedLines = new List<string>();
            bool addLines = true;
            foreach (string currentLine in serverLuaLines)
            {
                if (currentLine.StartsWith(AvorionServerManagerModificationStart)|| currentLine.StartsWith(AvorionServerManagerEditedNoticeStart))
                {
                    addLines = false;//ignore following lines
                }
                if (addLines)
                {
                    cleanedLines.Add(currentLine);
                }
                if(currentLine.StartsWith(AvorionServerManagerModificationEnd) || currentLine.StartsWith(AvorionServerManagerEditedNoticeEnd))
                {
                    addLines = true;
                }
               
            }
            return cleanedLines;

        }
        public List<string> PatchServerLua(List<string> serverLuaLines)
        {
            List<string> fileLinesWithPlaceHolder=new List<string>();
            bool addLines = true;
            bool wasModifiedFile = false;
            foreach(string currentLine in serverLuaLines)
            {
                if (currentLine.StartsWith(AvorionServerManagerModificationStart))
                {
                    addLines = false;//ignore following lines
                    wasModifiedFile = true;
                }
                if (addLines|| currentLine.StartsWith(AvorionServerManagerCheckDelayIdentifier) || currentLine.StartsWith(AvorionServerManagerCheckCommandsFunctionIdentifier) || currentLine.StartsWith(AvorionServerManagerUpdateLogicIdentifier))
                {
                        fileLinesWithPlaceHolder.Add(currentLine);
                }
                if (currentLine.StartsWith(AvorionServerManagerModificationEnd))
                {
                    addLines = true;
                }
            }
            if (wasModifiedFile)
            {
                return ApplyPatchesToAsmPlaceholderServerFile(fileLinesWithPlaceHolder);
            }else
            {
                return ApplyPatchesToVanillaFileServer(serverLuaLines);
            }
        }
        private List<string> ApplyPatchesToVanillaFileServer(List<string> vanillaFileLines)
        {
            List<string> patchedLines = new List<string>();
            patchedLines.Add(AvorionServerManagerEditedNoticeStart);
            patchedLines.Add("--This File was modified by the Avorion Server Manager");
            patchedLines.Add("--Please do not delete or add Lines beetween the comments: asm_modification_start and asm_modification_end");
            patchedLines.Add(AvorionServerManagerEditedNoticeEnd);
            foreach(string currentLine in vanillaFileLines)
            {
                
                if (currentLine.StartsWith(VanillaOnStartupFunctionStart))
                {
                    patchedLines.Add(AvorionServerManagerModificationStart);
                    patchedLines.Add(AvorionServerManagerCheckCommandsFunctionIdentifier);
                    patchedLines.AddRange(GetCheckCommandsFunctionLines());
                    patchedLines.Add(AvorionServerManagerModificationEnd);
                    patchedLines.Add(currentLine);
                    patchedLines.Add(AvorionServerManagerModificationStart);
                    patchedLines.Add(AvorionServerManagerCheckDelayIdentifier);
                    patchedLines.Add(_setCommandCheckDelayCodeInitial);
                    patchedLines.Add(AvorionServerManagerModificationEnd);
                }
                else if (currentLine.StartsWith(VanillaUpdateFunctionStart))
                {
                    patchedLines.Add(currentLine);
                    patchedLines.Add(AvorionServerManagerModificationStart);
                    patchedLines.Add(AvorionServerManagerUpdateLogicIdentifier);
                    patchedLines.AddRange(GetUpdateLogicLines());
                    patchedLines.Add(AvorionServerManagerModificationEnd);
                }else
                {
                    patchedLines.Add(currentLine);
                }
            }
            return patchedLines;
        }

        public List<string> ApplyPatchesToAsmPlaceholderServerFile(List<string> fileLinesWithPlaceholder)
        {
            List<string> patchedFileLines=new List<string>();
            foreach(string currentLine in fileLinesWithPlaceholder)
            {
                if (currentLine.StartsWith(AvorionServerManagerCheckDelayIdentifier))
                {
                    patchedFileLines.Add(AvorionServerManagerModificationStart);
                    patchedFileLines.Add(currentLine);
                    patchedFileLines.Add(_setCommandCheckDelayCodeInitial);
                    patchedFileLines.Add(AvorionServerManagerModificationEnd);
                }else if (currentLine.StartsWith(AvorionServerManagerCheckCommandsFunctionIdentifier))
                {
                    patchedFileLines.Add(AvorionServerManagerModificationStart);
                    patchedFileLines.Add(currentLine);
                    patchedFileLines.AddRange(GetCheckCommandsFunctionLines());
                    patchedFileLines.Add(AvorionServerManagerModificationEnd);
                }else if (currentLine.StartsWith(AvorionServerManagerUpdateLogicIdentifier))
                {
                    patchedFileLines.Add(AvorionServerManagerModificationStart);
                    patchedFileLines.Add(currentLine);
                    patchedFileLines.AddRange(GetUpdateLogicLines());
                    patchedFileLines.Add(AvorionServerManagerModificationEnd);
                }else
                {
                    patchedFileLines.Add(currentLine);
                }
            }
            return patchedFileLines;
        }
        private List<string> GetUpdateLogicLines()
        {
            List<string> updateLogicLines = new List<string>();
            updateLogicLines.Add("print(\""+Constants.UpdateTickIdentifier+"\")");
            updateLogicLines.Add("commandCheckDelay= Server():getValue(\"commandCheckDelay\")");
            updateLogicLines.Add("commandCheckDelay = commandCheckDelay - timeStep");
            updateLogicLines.Add("Server():setValue(\"commandCheckDelay\", commandCheckDelay)");
            updateLogicLines.Add("if commandCheckDelay < 0 then");
            updateLogicLines.Add(_setCommandCheckDelayCodeInitial);
            updateLogicLines.Add("checkForCommands()");
            updateLogicLines.Add("end");
            return updateLogicLines;
        }
        private List<string> GetCheckCommandsFunctionLines()
        {
            List<string> functionLines = new List<string>();
            functionLines.Add("function checkForCommands()");
            functionLines.Add("print(\""+Constants.CommandRequest+"\")");
            functionLines.Add("local command = io.read()");
            bool firstCommand = true;
            foreach(AvorionServerCommandDefinition currentDefinition in _commandDefinitions)
            {
                if (firstCommand)
                {
                    functionLines.Add("if command == \"" + currentDefinition.Id + "\" then");
                    firstCommand = false;
                }else
                {
                    functionLines.Add("elseif command == \"" + currentDefinition.Id + "\" then");
                }
                string parameterString = string.Empty;
                if (currentDefinition.ParameterNames != null)
                {
                    
                    foreach(string currentParameterName in currentDefinition.ParameterNames)
                    {
                        functionLines.Add("local "+currentParameterName+"=io.read()");
                        parameterString += currentParameterName + ",";
                    }
                    functionLines.Add(currentDefinition.LuaName + "(" + parameterString.TrimEnd(',') + ")");
                }else
                {
                    functionLines.Add(currentDefinition.LuaName + "()");
                }
            }
            functionLines.Add("end");
            functionLines.Add("end");
            return functionLines;
        }
    }
}
