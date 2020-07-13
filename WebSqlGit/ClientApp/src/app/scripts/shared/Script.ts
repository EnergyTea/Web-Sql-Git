
export interface Script {
  versionId: number;
  scriptId: number;
  name: string;
  body: string;
  author: string;
  isAuthor: boolean;
  tags: string[];
  categoryId: number;
  version: number;
  updateDataTime: Date;
  isLastVersion: boolean;
  creationDataTime: Date;
}
