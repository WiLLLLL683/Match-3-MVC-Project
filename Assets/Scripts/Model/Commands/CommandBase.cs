namespace Model.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected bool isValid = false;
        protected bool isExecuted = false;

        public void Execute()
        {
            if (!isValid)
                return;

            if (isExecuted)
                return;

            OnExecute();
        }

        public void Undo()
        {
            if (!isValid)
                return;

            if (!isExecuted)
                return;

            OnUndo();
        }

        protected abstract void OnExecute();
        protected abstract void OnUndo();
    }
}