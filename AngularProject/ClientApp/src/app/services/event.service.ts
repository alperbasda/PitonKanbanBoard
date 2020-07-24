import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PostLoginModel, DataResponse, UserCalenderModel, PostCalenderModel } from "../models/models";
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class EventService {
  private apiUrl: string;


  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.apiUrl = baseUrl + "api/UserCalenders/";
  }

  getEvents(userId: string): Observable<DataResponse> {
    return this.httpClient.get<DataResponse>(this.apiUrl + "events?userId=" + userId);
  }
  deleteEvent(id: string, userId: string): Observable<DataResponse> {
    return this.httpClient.get<DataResponse>(this.apiUrl + "delete?id=" + id + "&userId=" + userId);
  }

  insertEvent(model: PostCalenderModel): Observable<DataResponse> {
    return this.httpClient.get<DataResponse>(this.apiUrl + "insert?userId=" + model.UserId + "&description=" + model.Description + "&recordType=" + model.RecordType);
  }

}
