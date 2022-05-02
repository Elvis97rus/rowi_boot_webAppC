<script>
import { ref } from "vue";
import { HTTP } from "../api/http-common";
import MedicinesItem from "../components/MedicinesItem.vue";

export default {
  components: { MedicinesItem },
  data() {
    return {
      medicines: "",
      name: "",
      useMethod: "",
      effects: "",
      sideEffects: "",
      errors: [],
      newMedicine: 0,
    };
  },
  props: ["id"],

  // Fetches data when the component is created.
  created() {
    this.init();
  },
  methods: {
    init() {
      this.getMedicines();
    },
    dateChanged() {
      console.log(this.date);
      return this.date;
    },
    modalClose() {
      console.log("modalClose()");
      this.newMedicine = 0;
    },
    getPatients() {
      HTTP.get(`/api/patients`)
        .then((response) => {
          this.patients = response.data;
          return this.patients;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getMedicines() {
      HTTP.get(`/api/medicines`)
        .then((response) => {
          this.medicines = response.data;
          return this.medicines;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getSideEffectById() {},
  },
};
</script>

<template>
  <div>
    <div class="details flex justify-center">
      <button
        class="bg-green-500 hover:bg-green-900 px-2 py-2 rounded-md flex text-white"
        @click="this.newMedicine = 1"
      >
        <i class="px-1 self-center"><DocumentationIcon /></i>
        New Medicine
      </button>
    </div>
    <div class="flex flex-col" v-if="!this.$route.params.id">
      <MedicinesItem
        v-for="medicine of this.medicines"
        :key="medicine.id"
        :data="medicine"
      />
    </div>
    <div class="flex flex-col" v-else>
      <label for="searchBy"></label>
      <input id="searchBy" type="text" name="searchBy" />
      <span
        v-for="medicine in this.medicines"
        :key="medicine.id"
        class="w-full"
      >
        <MedicinesItem
          :data="medicine"
          v-if="medicine.id == this.$route.params.id"
        />
      </span>
    </div>
    <div
      v-if="this.newMedicine"
      class="fixed z-10 inset-0 overflow-y-auto"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
    >
      <div
        class="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0"
      >
        <div
          class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
          aria-hidden="true"
        ></div>
        <span
          class="hidden sm:inline-block sm:align-middle sm:h-screen"
          aria-hidden="true"
          >&#8203;</span
        >
        <div
          class="relative inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full"
        >
          <div class="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
            <div class="sm:flex sm:items-start">
              <MedicinesItem
                :data="{}"
                :popup="1"
                @modalClose="this.modalClose()"
                @medicinesRefresh="this.getMedicines()"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
