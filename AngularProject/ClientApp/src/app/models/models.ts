export class DataResponse {
    data: object;
    success: boolean;
    message: string;
}

export enum RecordType { Gun, Hafta, Ay }

export class UserCalenderModel {
  id: number;
  description: string;
  recordType: RecordType;
  recordTypeInt: number;
}

export class PostLoginModel {
  EMail: string;
  Password:string;
}

export class PostCalenderModel {
  Description: string;
  RecordType: string;
  UserId:string;
}
