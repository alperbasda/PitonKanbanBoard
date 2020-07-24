export class DataResponse {
    Data: object;
    Success: boolean;
    Message: string;
}

export enum RecordType { Gun, Hafta, Ay }

export class UserCalenderModel {
  Id: number;
  Description: string;
  RecordType: RecordType;
  RecordTypeInt: number;
}

export class PostLoginModel {
  EMail: string;
  Password:string;
}
