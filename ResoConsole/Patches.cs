using Elements.Core;
using HarmonyLib;
using System.Diagnostics;
using System.Reflection;

namespace ResoConsole
{
    namespace Patches
    {
        class ResoniteModLoader_Logger
        {
            public static void CheckAndPatch(Harmony harmony)
            {
                Assembly RMLLoggerAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(dll => dll.GetType("ResoniteModLoader.Logger") != null);

                if (RMLLoggerAssembly == default)
                    return;

                Type internalLogType = RMLLoggerAssembly.GetType();

                //if (Harmony.DEBUG == true)
                //{
                //    MethodInfo[] methodsInf = internalLogType.GetMethods(AccessTools.all);
                //    foreach (MethodInfo m in methodsInf)
                //    {
                //        Console.Write(m.ReturnType.Name + " " + m.Name + "(");
                //        ParameterInfo[] par = m.GetParameters();
                //        if (par.Length != 0)
                //        {
                //            foreach (ParameterInfo p in par)
                //            {
                //                Console.Write(p.ParameterType.Name + " " + p.Name + ", ");
                //            }
                //            Console.Write("\b\b  \b\b");
                //        }
                //        Console.Write("); ");
                //        Console.WriteLine("IsPrivate: " + m.IsPrivate + ", IsStatic: " + m.IsStatic);
                //    }
                //}

                MethodInfo method = internalLogType.GetMethod("SourceFromStackTrace", AccessTools.all);
                if (method == null)
                    return;

                func = method;

                // Nah we do it my way
                MethodInfo DebugFuncInternalIM = internalLogType.GetMethod("DebugFuncInternal", AccessTools.all);
                MethodInfo DebugFuncExternalIM = internalLogType.GetMethod("DebugFuncExternal", AccessTools.all);
                MethodInfo DebugInternalIM = internalLogType.GetMethod("DebugInternal", AccessTools.all);
                MethodInfo DebugExternalIM = internalLogType.GetMethod("DebugExternal", AccessTools.all);
                MethodInfo DebugListExternalIM = internalLogType.GetMethod("DebugListExternal", AccessTools.all);

                MethodInfo MsgInternalIM = internalLogType.GetMethod("MsgInternal", AccessTools.all);
                MethodInfo MsgExternalIM = internalLogType.GetMethod("MsgExternal", AccessTools.all);
                MethodInfo MsgListExternalIM = internalLogType.GetMethod("MsgListExternal", AccessTools.all);

                MethodInfo WarnInternalIM = internalLogType.GetMethod("WarnInternal", AccessTools.all);
                MethodInfo WarnExternalIM = internalLogType.GetMethod("WarnExternal", AccessTools.all);
                MethodInfo WarnListExternalIM = internalLogType.GetMethod("WarnListExternal", AccessTools.all);

                MethodInfo ErrorInternalIM = internalLogType.GetMethod("ErrorInternal", AccessTools.all);
                MethodInfo ErrorExternalIM = internalLogType.GetMethod("ErrorExternal", AccessTools.all);
                MethodInfo ErrorListExternalIM = internalLogType.GetMethod("ErrorListExternal", AccessTools.all);

                harmony.Patch(DebugFuncInternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.DebugFuncInternal), AccessTools.all)));
                harmony.Patch(DebugFuncExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.DebugFuncExternal), AccessTools.all)));
                harmony.Patch(DebugInternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.DebugInternal), AccessTools.all)));
                harmony.Patch(DebugExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.DebugExternal), AccessTools.all)));
                harmony.Patch(DebugListExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.DebugListExternal), AccessTools.all)));

                harmony.Patch(MsgInternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.MsgInternal), AccessTools.all)));
                harmony.Patch(MsgExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.MsgExternal), AccessTools.all)));
                harmony.Patch(MsgListExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.MsgListExternal), AccessTools.all)));

                harmony.Patch(WarnInternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.WarnInternal), AccessTools.all)));
                harmony.Patch(WarnExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.WarnExternal), AccessTools.all)));
                harmony.Patch(WarnListExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.WarnListExternal), AccessTools.all)));

                harmony.Patch(ErrorInternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.ErrorInternal), AccessTools.all)));
                harmony.Patch(ErrorExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.ErrorExternal), AccessTools.all)));
                harmony.Patch(ErrorListExternalIM, new HarmonyMethod(typeof(ResoniteModLoader_Logger).GetMethod(nameof(ResoniteModLoader_Logger.ErrorListExternal), AccessTools.all)));

//                UniLog.Log("ResoConsole INFO");
//                UniLog.Warning("ResoConsole WARN");
//                UniLog.Error("ResoConsole ERROR");
//#pragma warning disable CS8602
//                DebugExternalIM.Invoke(null, new object[] { "ResoConsole DEBUG" });
//#pragma warning restore CS8602
            }
            public enum LogType
            {
                Debug,
                Info,
                Warn,
                Error
            }

            #region Patched functions
#pragma warning disable CS8618 // No just no
#pragma warning disable CS8602
            internal static MethodInfo func;
#pragma warning disable CS8602
            internal delegate string SourceFromStackTrace_(StackTrace stackTrace);
            internal static SourceFromStackTrace_ SourceFromStackTrace = (stackTrace) => {
#pragma warning disable CS8603 // Nah
                return (string)func.Invoke(null, new[] { stackTrace });
#pragma warning restore CS8603
#pragma warning restore CS8618
            }; // fake it until you make it

            internal static bool DebugFuncInternal(Func<string> messageProducer)
            {
                LogInternal(LogType.Debug, messageProducer());
                return false;
            }

            internal static bool DebugFuncExternal(Func<object> messageProducer)
            {
                LogInternal(LogType.Debug, messageProducer(), SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool DebugInternal(string message)
            {
                LogInternal(LogType.Debug, message);
                return false;  // Override the default function. Yes sir. I AM THE CAPTAIN NOW!
            }

            internal static bool DebugExternal(object message)
            {
                LogInternal(LogType.Debug, message, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool DebugListExternal(object[] messages)
            {
                LogListInternal(LogType.Debug, messages, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool MsgInternal(string message)
            {
                LogInternal(LogType.Info, message);
                return false;
            }

            internal static bool MsgExternal(object message)
            {
                LogInternal(LogType.Info, message, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool MsgListExternal(object[] messages)
            {
                LogListInternal(LogType.Info, messages, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool WarnInternal(string message)
            {
                LogInternal(LogType.Warn, message);
                return false;
            }

            internal static bool WarnExternal(object message)
            {
                LogInternal(LogType.Warn, message, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool WarnListExternal(object[] messages)
            {
                LogListInternal(LogType.Warn, messages, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool ErrorInternal(string message)
            {
                LogInternal(LogType.Error, message);
                return false;
            }

            internal static bool ErrorExternal(object message)
            {
                LogInternal(LogType.Error, message, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }

            internal static bool ErrorListExternal(object[] messages)
            {
                LogListInternal(LogType.Error, messages, SourceFromStackTrace(new StackTrace(1)));
                return false;
            }
            #endregion

            #region Do not patch these
            internal static void LogInternal(LogType logType, object message, string source = null)
            {
                if (message == null)
                {
                    message = "null";
                }

                string[] logMsgStore = new[] { "[DEBUG]", "[INFO]", "[WARN]", "[ERROR]" };

                string output = source == null ? $"{logMsgStore[(int)logType]}[ResoniteModLoader] {message}" :
                    $"{logMsgStore[(int)logType]}[ResoniteModLoader/{source}] {message}";

                switch (logType)
                {
                    case LogType.Error:
                        {
                            UniLog.Error(output);
                            break;
                        }
                    case LogType.Warn:
                        {
                            UniLog.Warning(output);
                            break;
                        }

                    case LogType.Debug:
                        {
                            // Duck Tape PLEASE
                            UniLog.OnLog -= ConsoleController.WriteLineL;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            ConsoleController.WriteLine(message);
                            UniLog.Log(message);
                            UniLog.OnLog += ConsoleController.WriteLineL;
                            break;
                        }
                    default:
                        {
                            UniLog.Log(output);
                            break;
                        }
                }
            }

            internal static bool LogListInternal(LogType logType, object[] messages, string source = null)
            {
                foreach (object msg in messages)
                {
                    LogInternal(logType, msg, source);
                }

                return false;
            }
            #endregion
        }

        class MonkeyLoader_Logging
        {
            public void Debug(object __instance, Func<object> meeeageProducer)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }

            public void Error(Func<object> meeeageProducer)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }

            public void Fatal(Func<object> meeeageProducer)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            public void Info(Func<object> meeeageProducer)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            public void Trace(Func<object> meeeageProducer)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }

            public void Warn(Func<object> meeeageProducer)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }
    }
}