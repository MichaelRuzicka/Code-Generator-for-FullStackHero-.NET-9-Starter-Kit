namespace CodeGen
{
    public interface ITemplateLineHandler
    {
        void DoTemplating(BuilderParams builderparams,ref string templateLine);
    }
}