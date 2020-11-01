// объединил в 1 файл
export class ToolType {
  constructor(
    public id?: string,
    public name?: string,
    public mainType?: boolean[],
    public secondaryType?: number,
    public brands?: string[], 
    public serveCost?: SC_Dictionary,
    public imgRefenrence?: string
  ) { 
      this.mainType = [false, false, false, false, false, false, false, false];
      this.serveCost = new SC_Dictionary();
   }
}

export class SC_Dictionary {
  constructor(
    public keys?: string[],
    public values?: string[]
  ) { 
    this.keys = [];
    this.values = [];
  }
}
