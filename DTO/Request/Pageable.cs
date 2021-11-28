public class Pageable
{

    public int Page;

    public int Size;


    public Pageable() { }

    public Pageable(int page, int? size)
    {
        Page = page;
        Size = size ?? 10;
    }

};