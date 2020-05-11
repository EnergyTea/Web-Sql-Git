
export interface Script {
  id: number;
  scriptId: number;
  name: string;
  body: string;
  author: string;
  tags: string[];
  categoryId: number;
  version: number;
  updateDataTime: Date;
  isLastVersion: boolean;
  creationDataTime: Date;
}
