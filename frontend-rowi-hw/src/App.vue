<script setup>
import { RouterLink, RouterView } from "vue-router";
import HelloWorld from "@/components/HelloWorld.vue";
</script>

<template>
  <div>
    <header>
      <div>
        <HelloWorld class="bg-gray-200 rounded-md shadow-sm my-5" />

        <nav
          class="w-4/5 flex justify-around m-auto py-4 text-lg bg-white rounded-md shadow-sm my-5"
        >
          <RouterLink
            class="hover:ring hover:bg-gray-100 hover:ring-gray-300 px-5"
            to="/"
            >Home</RouterLink
          >
          <RouterLink
            class="hover:ring hover:bg-gray-100 hover:ring-gray-300 px-5"
            to="/doctors"
            >Doctors</RouterLink
          >
          <RouterLink
            class="hover:ring hover:bg-gray-100 hover:ring-gray-300 px-5"
            to="/patients"
            >Patients</RouterLink
          >
          <RouterLink
            class="hover:ring hover:bg-gray-100 hover:ring-gray-300 px-5"
            to="/medicines"
            >Medicines</RouterLink
          >
          <RouterLink
            class="hover:ring hover:bg-gray-100 hover:ring-gray-300 px-5"
            to="/visits"
            >Visits</RouterLink
          >
        </nav>
      </div>
    </header>
    <div class="body m-auto">
      <RouterView />
    </div>
  </div>
</template>

<style>
@import "@/assets/base.css";
</style>
