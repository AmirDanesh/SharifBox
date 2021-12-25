import { ToastComponent } from './../../../shared/components/toast/toast.component';
import { FormControl } from '@angular/forms';
import { NewsService } from './../../../shared/services/news.service';
import { FormGroup, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChangeEvent, CKEditorComponent } from '@ckeditor/ckeditor5-angular';
import * as InlineEditor from '@ckeditor/ckeditor5-build-inline';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {
  public Editor = InlineEditor;
  // public Editor = InlineEditor.create(document.querySelector( '#editor' ),{
  //   // This value must be kept in sync with the language defined in webpack.config.js.
  //   language: 'fa',
  //   toolbar: [ 'heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote' ],
  //   heading: {
  //       options: [
  //           { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
  //           { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
  //           { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' }
  //       ]
  //   },
  //   plugins: [ SimpleUploadAdapter ],
  //   simpleUpload: {
  //     uploadUrl: 'http://localhost:5062/api/Files',
  //     withCredentials: true,
  //     headers: {
  //       'X-CSRF-TOKEN': 'CSFR-Token',
  //        Authorization: 'Bearer <JSON Web Token>',
  //     }
  //   }
  // });
  editorConfig = {
    // This value must be kept in sync with the language defined in webpack.config.js.
    language: 'fa',
    // plugins: [ SimpleUploadAdapter ],
    simpleUpload: {
      uploadUrl: 'http://demo.borhansoft.ir:5062/api/Files',
      withCredentials: true,
      headers: {
        'X-CSRF-TOKEN': 'CSFR-Token',
        Authorization: 'Bearer <JSON Web Token>',
      }
    }
  }
  newsForm: FormGroup;
  editMode = false;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  id = '';


  editorContent = '<p>Hi ...</p>';
  @ViewChild('editor') editorComponent!: CKEditorComponent;

  constructor(private newsService: NewsService,
    private route: ActivatedRoute,
    private router: Router,
    private _snackBar: MatSnackBar,) {
    this.newsForm = new FormGroup({
      id: new FormControl(null),
      title: new FormControl(null, Validators.required),
      content: new FormControl(null, Validators.required),
      startDate: new FormControl(null),
      endDate: new FormControl(null),
      showOnLanding: new FormControl(false, Validators.required),
      eventBanner: new FormControl({})
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = params.id;
        if (this.id) {
          this.editMode = true;
          this.newsForm.controls.eventBanner = new FormControl(`http://demo.borhansoft.ir:5080/eventBanner/${this.id}`);
        } else {
          this.newsForm.controls.eventBanner = new FormControl('/assets/images/Profile_avatar_placeholder_large (1).png');
        }

      }
    );
    this.getSelectedNews();
  }


  // image file
  showPreview(event: any) {
    const file = (event.target).files[0];
    this.newsForm.patchValue({
      avatar: file
    });
    this.newsForm.get('avatar')?.updateValueAndValidity();

    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      // this.imageURL = reader.result as string;
      this.newsForm.patchValue({ eventBanner: reader.result });
    };
    reader.readAsDataURL(file);
    console.log(this.newsForm.get('eventBanner')?.value);
  }



  // tslint:disable-next-line: typedef
  addNews() {
    this.newsService.addNews(this.newsForm.value)
      .subscribe(
        nxt => {
          this.router.navigate(['admin', 'news-list']),

            this._snackBar.openFromComponent(ToastComponent, {
              data: {
                text: 'درخواست با موفقیت انجام شد'
              }
            });
        },
        err => this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: err.error
          }
        }));
  }


  // tslint:disable-next-line: typedef
  updateNews() {
    this.newsService.updateNews(this.newsForm.value, this.id)
      .subscribe(
        nxt => {
          this.newsForm.patchValue(nxt);
          this.router.navigate(['admin', 'news-list']);
          this._snackBar.openFromComponent(ToastComponent, {
            data: {
              text: 'درخواست با موفقیت انجام شد'
            }
          });
        },
        err => this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: err.error
          }
        })
      );
  }


  // tslint:disable-next-line: typedef
  submitNews() {
    this.newsForm.markAllAsTouched();
    if (!this.newsForm.valid)
      return;

    if (this.editMode) {
      this.updateNews();
    } else {
      this.addNews();
    }



  }


  // tslint:disable-next-line: typedef
  getSelectedNews() {
    if (this.editMode) {
      this.newsService.getSelectedNews(this.id).subscribe(
        (nxt: any) => {
          this.newsForm.patchValue(nxt);
          this.editorComponent.editorInstance?.setData(nxt.content);
        }
      );
    }
  }

  // tslint:disable-next-line: typedef
  contentChange({ editor }: ChangeEvent) {

    const content = editor.getData();
    this.newsForm.patchValue({ content });
    // tslint:disable-next-line: no-unused-expression
  }

  ckTouch() {
    this.newsForm.get('content')?.markAsTouched();
  }
}


