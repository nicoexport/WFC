namespace Runtime {
    public interface IInput {
        IRule[] Read(out IState[] possibleStates);
    }
}