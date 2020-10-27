// объединил в 1 файл
export class ToolType {
  constructor(
    public id?: string,
    public name?: string,
    public mainType?: number,
    public secondaryType?: number,
    public brands?: string[], 
    public serveCost?: SC_Dictionary,
    public imgRefenrence?: string
  ) { }
}

export class SC_Dictionary {
  constructor(
    public keys?: string[],
    public values?: string[]
  ) { }
}
