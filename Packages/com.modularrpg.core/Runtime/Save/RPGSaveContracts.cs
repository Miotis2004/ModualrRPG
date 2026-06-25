namespace ModularRPG.Core
{
    public interface IRPGSaveData
    {
        string Version { get; }
    }

    public interface IRPGSaveProvider
    {
        string SaveProviderId { get; }
        IRPGSaveData CaptureState();
        void RestoreState(IRPGSaveData saveData);
    }
}
