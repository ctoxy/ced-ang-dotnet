import { Component, OnInit, Output, EventEmitter} from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  // definition du reactive form
  registerForm: FormGroup;

  constructor( private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('Hello', Validators.required),
      password: new FormControl('',
      [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confirmPassword: new FormControl('', Validators.required),

    }, this.passwordMatchValidator);
  }
  /* custom validator to compare  password and confirm password */
  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
  }

  register() {
    /*this.authService.register(this.model).subscribe(() => {
      this.alertify.success('registration ok');
    }, error => {
      this.alertify.error(error);
    });*/
    console.log(this.registerForm.value);

  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.message('cancelled');

  }

}
