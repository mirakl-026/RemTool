import { Dictionary } from './dictionary';

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
