// объединил в 1 файл
export class ToolType {
  constructor(
    public Id?: string,
    public Name?: string,
    public MainType?: number,
    public SecondaryType?: number,
    public Brands?: string[], 
    public ServeCost?: Dictionary,
    public ImgRefenrence?: string
  ) { }
}

export class Dictionary {
  constructor(
    public Keys?: string[],
    public Values?: string[]
  ) { }
}
