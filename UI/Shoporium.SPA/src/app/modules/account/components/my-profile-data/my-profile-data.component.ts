import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Account } from '../../models/account';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-my-profile-data',
  templateUrl: './my-profile-data.component.html',
  styleUrls: ['./my-profile-data.component.css']
})
export class MyProfileDataComponent implements OnInit {
  form = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    mobileNumber: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required]),
    password: new FormControl(''),
    newPassword: new FormControl(''),
    confirmPassword: new FormControl('')
  });

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.getUserInfo().subscribe((res: Account) => {
        this.form.patchValue(res);
      }
    );
  }

  save() {
    const model = this.form.value;
    this.accountService.updateUserInfo(model).subscribe(() => {
      
    })
  }
}
