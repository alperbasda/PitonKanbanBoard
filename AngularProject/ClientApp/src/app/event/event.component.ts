import { Component, OnInit } from '@angular/core';
import { EventService } from "../services/event.service";
import { AuthService } from "../services/auth.service";
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

import {  UserCalenderModel, PostCalenderModel } from "../models/models";
import { Router } from '@angular/router';


@Component({
  selector: 'event',
  templateUrl: './event.component.html',
  providers: [EventService,AuthService]
})
export class EventComponent implements OnInit {

  
  constructor(private eventService: EventService, private authService: AuthService, private router: Router, private formBuilder: FormBuilder) { }

  postEvent: PostCalenderModel;
  postEventForm: FormGroup;

  toDayEvents: UserCalenderModel[];
  toWeekEvents: UserCalenderModel[];
  toMonthEvents: UserCalenderModel[];

  ngOnInit(): void {
    
    this.refreshItems();
    this.createForm();
  }

  createForm() {
    this.postEventForm = this.formBuilder.group
    ({
      Description: ["", Validators.required],
      RecordType: ["", Validators.required],
    });
  }

  refreshItems() {
    let token = this.authService.getToken();

    if (token != null && token.length > 0) {

      this.eventService.getEvents(this.authService.getToken()).subscribe(response => {
        debugger;
        if (response.success) {
          this.toDayEvents = (response.data as UserCalenderModel[]).filter(item => item.recordType == 0);
          this.toWeekEvents = (response.data as UserCalenderModel[]).filter(item => item.recordType == 1);
          this.toMonthEvents = (response.data as UserCalenderModel[]).filter(item => item.recordType == 2);
        } else {
          alert(response.message);
        }

      });
    } else {
      this.router.navigate(['/login']);
    }
  }

  delete(item: UserCalenderModel) {

    let token = this.authService.getToken();

    if (token != null && token.length > 0) {

      this.eventService.deleteEvent(item.id.toString(),token).subscribe(w => {
        if (w.success) {
          this.refreshItems();
        } else {
          alert(w.message);
        }
      });
    } else {
      this.router.navigate(['/login']);
    }

  }

  insertData() {

    let token = this.authService.getToken();

    if (token != null && token.length > 0) {
      this.postEvent = Object.assign({}, this.postEventForm.value);
      this.postEvent.UserId = token;
      debugger;
      this.eventService.insertEvent(this.postEvent).subscribe(w => {
        if (w.success) {
          this.refreshItems();
        } else {
          alert(w.message);
        }
      });
    } else {
      this.router.navigate(['/login']);
    }

  }

 



}
