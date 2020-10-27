// объединил в 1 файл
export class ToolType {
  constructor(
    public Id?: string,
    public Name?: string,
    public MainType?: number,
    public SecondaryType?: number,
    public Brands?: string[], 
    public ServeCost?: SC_Dictionary,
    public ImgRefenrence?: string
  ) { }
}

export class SC_Dictionary {
  constructor(
    public Keys?: string[],
    public Values?: string[]
  ) { }
}
