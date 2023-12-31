from django.urls import path

from . import views

urlpatterns = [
    path("", views.index, name="index"),
    path("predict", views.predict_marks, name="predict_marks"),
    path("analyze", views.student_performance_analyze, name="student_performance_analyze"),
]