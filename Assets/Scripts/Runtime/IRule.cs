namespace Runtime {
    public interface IRule {
        IState FirstState { get; }
        IState SecondState { get; }
        bool Test(IState first, IState second);
    }
}