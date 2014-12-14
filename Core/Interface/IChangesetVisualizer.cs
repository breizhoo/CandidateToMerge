namespace Core.Interface
{
    public interface IChangesetVisualizer
    {
        bool IsVisualizerAvailable { get; }
        void Execute(int changesetId);
    }
}