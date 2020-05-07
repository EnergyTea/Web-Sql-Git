
export interface Script {
  id: number;
  name: string;
  body: string;
  author: string;
  categoryId: number;
  version: number;
  updateDataTime: Date;
  isLastVersion: boolean;
  creationDataTime: Date;
}
