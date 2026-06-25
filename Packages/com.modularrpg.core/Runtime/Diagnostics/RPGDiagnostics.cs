using System;
using UnityEngine;

namespace ModularRPG.Core
{
    public enum RPGLogCategory
    {
        Initialization,
        Lifecycle,
        Registry,
        Configuration,
        Validation,
        Error
    }

    public interface IRPGDiagnostics
    {
        void Log(RPGLogCategory category, string message);
        void Warning(RPGLogCategory category, string message);
        void Error(RPGLogCategory category, string message);
    }

    public sealed class UnityRPGDiagnostics : IRPGDiagnostics
    {
        public void Log(RPGLogCategory category, string message) => Debug.Log(Format(category, message));
        public void Warning(RPGLogCategory category, string message) => Debug.LogWarning(Format(category, message));
        public void Error(RPGLogCategory category, string message) => Debug.LogError(Format(category, message));
        private static string Format(RPGLogCategory category, string message) => $"[ModularRPG:{category}] {message}";
    }

    public sealed class RPGDiagnosticsMessage
    {
        public RPGDiagnosticsMessage(RPGLogCategory category, string message)
        {
            Category = category;
            Message = message ?? string.Empty;
        }

        public RPGLogCategory Category { get; }
        public string Message { get; }
    }
}
