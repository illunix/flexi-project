import {
  Component,
  inject,
  input,
  output,
  ResourceStatus,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Guid } from '@microsoft/kiota-abstractions';
import { NgxControlError } from 'ngxtension/control-error';
import { explicitEffect } from 'ngxtension/explicit-effect';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { SkeletonModule } from 'primeng/skeleton';
import { Toast } from 'primeng/toast';
import { UsersService } from '../../data-access/users.service';

@Component({
  standalone: true,
  imports: [
    DialogModule,
    ButtonModule,
    SkeletonModule,
    InputTextModule,
    ReactiveFormsModule,
    NgxControlError,
    Toast,
    FileUploadModule,
  ],
  providers: [MessageService],
  selector: 'flexi-project-add-edit-user-dialog',
  templateUrl: './add-edit-user-dialog.component.html',
  styleUrl: './add-edit-user-dialog.component.scss',
})
export class AddEditUserDialogComponent {
  readonly usersService = inject(UsersService);
  readonly #msgService = inject(MessageService);
  readonly form = new FormGroup({
    name: new FormControl<string | undefined>(undefined, Validators.required),
    email: new FormControl<string | undefined>(undefined, [
      Validators.required,
      Validators.email,
    ]),
    avatar: new FormControl<string | undefined>(undefined),
  });

  readonly visible = input.required<boolean>();
  readonly close = output<void>();
  readonly userId = input<Guid>();

  constructor() {
    this.close.subscribe(() => this.form.reset());
    explicitEffect([this.usersService.addedUser.status], ([status]) => {
      switch (status) {
        case ResourceStatus.Resolved:
          this.#msgService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Successfully added user',
            life: 3000,
          });
          break;
        case ResourceStatus.Error:
          this.#msgService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong while adding user',
            life: 3000,
          });
          break;
      }
      this.close.emit();
    });
  }

  get f() {
    return this.form.controls;
  }

  get isEditMode(): boolean {
    return this.userId() !== null;
  }

  get header(): string {
    return this.isEditMode ? 'Edit' : 'Add';
  }

  onUploadItemImages(event: FileSelectEvent) {
    const file = event.currentFiles[0];
    const reader = new FileReader();

    reader.onload = () => {
      const base64String = reader.result as string;
      this.f.avatar.setValue(base64String);
    };

    reader.readAsDataURL(file);
  }

  onAddOrUpdate() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.usersService.addUser$.next(this.form.getRawValue());
  }
}
