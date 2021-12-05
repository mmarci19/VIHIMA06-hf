import { Component, Input, OnInit } from '@angular/core';
import { CommentDto } from 'src/app/shared';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
})
export class CommentComponent implements OnInit {
  @Input() comment: CommentDto = new CommentDto();
  constructor() {}

  ngOnInit(): void {}
}
