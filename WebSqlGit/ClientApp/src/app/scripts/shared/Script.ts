export interface Script {
  id: number;
  name: string;
  body: string;
  author: string;
  categoryId: number;
  version: number;
  updateDataTime: number;
  isLastVersion: boolean;
}
