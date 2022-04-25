<script setup>
import { RouterLink, RouterView } from "vue-router";
import HelloWorld from "@/components/HelloWorld.vue";
</script>

<template>
  <div>
    <header>
      <div class="">
        <HelloWorld />

        <nav>
          <RouterLink to="/">Home</RouterLink>
          <RouterLink to="/doctors">Doctors</RouterLink>
          <RouterLink to="/patients">Patients</RouterLink>
          <RouterLink to="/medicines">Medicines</RouterLink>
          <RouterLink to="/visits">Visits</RouterLink>
        </nav>
      </div>
    </header>
    <div class="body m-0-auto">
      <RouterView />
    </div>
  </div>
</template>

<style>
@import "@/assets/base.css";
</style>
