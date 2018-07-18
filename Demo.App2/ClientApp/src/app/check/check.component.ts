import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { DemoService } from '../../shared/demoService';
import { Check } from '../../shared/check.model';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { CheckOutput } from '../../shared/checkoutput.model';



@Component({
  selector: 'checkentry',
  templateUrl: './check.component.html',
  styleUrls: [ './check.component.css']
})

export class CheckComponent implements OnInit {
  @ViewChild('autoShownModal') autoShownModal: ModalDirective;
  isModalShown: boolean = false;
  closeResult: string;
  checkForm: FormGroup;
  successfulSave: boolean;
  errors: string[];
  public checkOutput: CheckOutput;

  constructor(private _fb: FormBuilder, private _demoService: DemoService) {
    this.closeResult = '';
    this.checkForm = this._fb.group({
      payee: ['', [Validators.required]],
      amount: [0, [Validators.min(.01), Validators.max(99999999999999.99)]],
      checkdate: ['', [Validators.required]]
    });
   
    this.errors = [];
    this.successfulSave = false;
   }


  showModal(data: CheckOutput): void {
    this.checkOutput = data;
    this.isModalShown = true;
  }

  hideModal(): void {
    this.autoShownModal.hide();
  }

  onHidden(): void {
    this.isModalShown = false;
  }

  ngOnInit() {

  }


  onSubmit() {
    let temp = this;
    if (this.checkForm.valid) {

      var checkInput: Check = {
        Id: '',
        Payee: this.checkForm.value.payee,
        Amount: this.checkForm.value.amount,
        CheckDate: new Date(this.checkForm.value.checkdate)
      }

      this._demoService.saveCheck(checkInput)
        .subscribe(
          (data: CheckOutput) => {

            this.successfulSave = true;

            this.showModal(data);
          },
          (err) => {
            this.successfulSave = false;
            if (err.status = 400) {
              let validationErrorDictionary = JSON.parse(err.text());
              for (var fieldName in validationErrorDictionary) {
                if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                  this.errors.push(validationErrorDictionary[fieldName]);
                }
              }

            }
            else {
              this.errors.push("something went wrong!");
            }
          });
    
    }
  }
}
