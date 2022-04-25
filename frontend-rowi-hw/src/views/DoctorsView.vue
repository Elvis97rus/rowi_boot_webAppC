<script>
import { ref } from "vue";
import { HTTP } from "../api/http-common";
import DoctorsItem from "../components/DoctorsItem.vue";

export default {
  components: { DoctorsItem },
  data() {
    return {
      date: "",
      visits: [],
      doctors: [],
      medicines: [],
      patients: [],
      errors: [],
      newDoctor: 0,
    };
  },
  props: ["id"],

  // Fetches data when the component is created.
  created() {
    this.init();
  },
  methods: {
    init() {
      this.getPatients();
      this.getVisits();
      this.getMedicines();
      this.getDoctors();
    },
    dateChanged() {
      console.log(this.date);
      return this.date;
    },
    modalClose() {
      console.log("modalClose()");
      this.newPatient = 0;
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
    getVisits() {
      HTTP.get(`/api/visits`)
        .then((response) => {
          this.visits = response.data;
          console.log(this.visits.filter(this.$route.params.id));
          return this.visits;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getDoctors() {
      HTTP.get(`/api/doctors`)
        .then((response) => {
          this.doctors = response.data;
          return this.doctors;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getVisitById() {},
  },
};
</script>

<template>
  <div>
    <div class="details flex justify-center">
      <i><DocumentationIcon /></i>
      <a href="/doctors"><h3>Doctors</h3></a>
      <span>
        ---
        <button class="btn btn-sucsess" @click="this.newDoctor = 1">
          New
        </button></span
      >
    </div>
    <div class="flex flex-col" v-if="!this.$route.params.id">
      <DoctorsItem
        v-for="doctor of this.doctors"
        :key="doctor.id"
        :data="doctor"
      />
    </div>
    <div class="flex flex-col" v-else>
      <span v-for="doctor in this.doctors" :key="doctor.id" class="w-full">
        <DoctorsItem :data="doctor" v-if="doctor.id == this.$route.params.id" />
      </span>
    </div>
    <!-- This example requires Tailwind CSS v2.0+ -->
    <div
      v-if="this.newDoctor"
      class="fixed z-10 inset-0 overflow-y-auto"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
    >
      <div
        class="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0"
      >
        <!--
      Background overlay, show/hide based on modal state.

      Entering: "ease-out duration-300"
        From: "opacity-0"
        To: "opacity-100"
      Leaving: "ease-in duration-200"
        From: "opacity-100"
        To: "opacity-0"
    -->
        <div
          class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
          aria-hidden="true"
        ></div>

        <!-- This element is to trick the browser into centering the modal contents. -->
        <span
          class="hidden sm:inline-block sm:align-middle sm:h-screen"
          aria-hidden="true"
          >&#8203;</span
        >

        <!--
      Modal panel, show/hide based on modal state.

      Entering: "ease-out duration-300"
        From: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
        To: "opacity-100 translate-y-0 sm:scale-100"
      Leaving: "ease-in duration-200"
        From: "opacity-100 translate-y-0 sm:scale-100"
        To: "opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
    -->
        <div
          class="relative inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full"
        >
          <div class="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
            <div class="sm:flex sm:items-start">
              <DoctorsItem
                :data="{}"
                :popup="1"
                @modalClose="this.modalClose()"
                @patientsRefresh="this.getDoctors()"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
