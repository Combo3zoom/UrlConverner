import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlsComponent } from './short-urls.component';

describe('ShortUrlsComponent', () => {
  let component: ShortUrlsComponent;
  let fixture: ComponentFixture<ShortUrlsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShortUrlsComponent]
    });
    fixture = TestBed.createComponent(ShortUrlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
