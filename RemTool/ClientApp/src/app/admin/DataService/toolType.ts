
export class ToolType {
  constructor(
    public Id?: string,
    public Name?: string,
    public MainType?: number,
    public SecondaryType?: number,
    public Brands?: string[],
    public Serve?: string[],
    public Cost?: string[],
    public ImgRefenrence?: string
  ) { }
}
