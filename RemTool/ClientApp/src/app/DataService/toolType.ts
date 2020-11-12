// объединил в 1 файл
export class ToolType {
  constructor(
    public id?: string,
    public name?: string,
    public mainType?: boolean[],
    public secondaryType?: number,
    public brands?: string[], 
    public serves?: string[],
    public costs?: string[],
    public imgRefenrence?: string,
    public info?: string
  ) { 
      this.mainType = [false, false, false, false, false, false, false, false];
      this.serves = [];
      this.costs = [];
   }
}