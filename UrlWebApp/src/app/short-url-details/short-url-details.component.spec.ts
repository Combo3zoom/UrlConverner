import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlDetailsComponent } from './short-url-details.component';

describe('ShortUrlDetailsComponent', () => {
  let component: ShortUrlDetailsComponent;
  let fixture: ComponentFixture<ShortUrlDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShortUrlDetailsComponent]
    });
    fixture = TestBed.createComponent(ShortUrlDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
