import { Component, Injectable, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { ResponseModel } from 'src/app/models/responseModel';
import { StackLimit } from 'src/app/models/stackLimit';
import { HttpService } from 'src/app/services/http.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UPDATE_STACK } from 'src/app/store/actions/stack.action';

@Injectable({ providedIn: 'root' })

@Component({
  selector: 'app-stack-settings',
  templateUrl: './stack-settings.component.html',
  styleUrls: ['./stack-settings.component.css']
})
export class StackSettingsComponent implements OnInit {

  isCollapsed: boolean = true;
  stackLimitList: StackLimit[] = [];
  stackData?: Observable<StackLimit[]>;

  stackForm: FormGroup;

  constructor(public service: HttpService,
    public fb: FormBuilder,
    public notification: NotificationService,
    public modalService: NgbModal,
    private store: Store<{ StackData: StackLimit[] }>
  ) {
    this.stackForm = this.fb.group({
      stackList: this.fb.array([])
    });
    this.stackData = this.store.select(data => data.StackData);
  }

  get stackList(): FormArray {
    return this.stackForm.get("stackList") as FormArray
  }

  stackPatchValue(value: StackLimit): FormGroup {
    return this.fb.group({
      id: [value.id, Validators.required],
      stakeLimitPrice: [value.stakeLimitPrice, Validators.required]
    })
  }

  ngOnInit(): void {
    this.stackData?.subscribe((stack: StackLimit[]) => {
      if (stack.length > 0) {
        this.stackLimitList = stack;
        if (this.stackList.controls.length == 0) {
          stack.forEach((x) => {
            this.stackList.push(this.stackPatchValue(x));
          })
        }
      }
    });
  }

  stackSubmit(): void {
    if (this.stackForm.controls.stackList.status == "INVALID") {
      this.notification.showError("Enter a valid number.");
      return;
    }
    this.store.dispatch(UPDATE_STACK({ state : this.stackLimitList , stack: this.stackForm.controls.stackList.value }));
    console.log(this.stackForm.controls);
    return;
    this.service.post('Setting/UpdateStakeLimit', this.stackForm.controls.stackList.value)
      .subscribe((response: ResponseModel) => {
        if (response.isSuccess == true) {
          this.notification.showSuccess(response.message);
          this.modalService.dismissAll();
        }
      });
  }

  openStackModal(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

}

